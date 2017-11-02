import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';  //Reactive Forms
import 'rxjs/add/operator/debounceTime';
import { CardLayout } from '../../shared/model/card-layout';
import { FormElements, FormGroupDetails, IFormGroupMetadata, FormGroupValid } from '../../shared/model/form-elements';
import { Subscription } from 'rxjs/Subscription';
import { ComponentMessageService } from '../../shared/services/component-message.service';
import { StudentApplicationDataLookup } from '../../shared/model/data-binding';
import { HomeDataService } from '../../shared/services/home-data.service';
import { AppConfigurableSettings } from '../../shared/services/app-configurable.settings';//'./app-configurable.settings';
import { PaginationValidation } from "../../shared/model/pagination-validation";

function emailMatcher(c: AbstractControl) {
    let emailControl = c.get('emailaddress1');
    let confirmControl = c.get('txtConfirmEmail');
    if (emailControl!.pristine || confirmControl!.pristine) {
        return null;
    }
    if (emailControl!.value === confirmControl!.value) {
        return null;
    }
    return { 'match': true };
}

@Component({
    selector: 'app-course-application',
    templateUrl: './course-application.component.html',
    styleUrls: ['./course-application.component.css']
})

export class CourseApplicationComponent implements OnInit, OnDestroy {
    caForm: FormGroup;

    private piGroupValid: boolean;
    private opiGroupValid: boolean;
    private setMessageOnControlSubscribeRef: Subscription;//any; // Subscribe method reference
    private checkErrorOnControlSubscribeRef: Subscription;// any; // Subscribe method reference
    private checkErrorOnGroupSubscribeRef: Subscription;//any; // Subscribe method reference

    paginationMessage: FormGroupDetails;
    paginationMessageSubscription: Subscription;
    vrt_studiedatkanganinstitutebendigotafebeforeSubscription: Subscription;

    private stdAppDataLkp: StudentApplicationDataLookup; // Loopup values from databse

    constructor(private fb: FormBuilder, private cms: ComponentMessageService, private hds: HomeDataService) {
    }

    ngOnInit(): void {

        //Create Form Controls
        this.caForm = this.fb.group({
            piGroup: this.fb.group({ //Personal Information
                vrt_title: ['', [Validators.required]],
                firstName: ['', [Validators.required]],
                lastname: ['', [Validators.required]],
                mobilephone: ['', [Validators.required]],
                emailGroup: this.fb.group({
                    emailaddress1: ['', [Validators.required, Validators.pattern("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")]],
                    txtConfirmEmail: ['', Validators.required],
                }, { validator: emailMatcher })
            }),
            opiGroup: this.fb.group({  // Other Personal Information
                birthdate: ['', [Validators.required, Validators.pattern(/^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/g)]],
                studentGender: ['', [Validators.required]],
                address1_postalcode: ['', [Validators.required]],
                vrt_whatbroughtyoutothekanganinstitutewebsite: ['', [Validators.required]],
                vrt_studiedatkanganinstitutebendigotafebefore: ['', [Validators.required]],
                vrt_kibtstudentidnumber : ['']
            }),
            resGroup: this.fb.group({
                residency: ['', [Validators.required]]
            })
        });

        // Conditional Validation - vrt_studiedatkanganinstitutebendigotafebefore
        this.vrt_studiedatkanganinstitutebendigotafebeforeSubscription =
            this.caForm.get('opiGroup.vrt_studiedatkanganinstitutebendigotafebefore')!.valueChanges
            .subscribe(value => this.sendNotificationToVrt_kibtstudentidnumber(value));

        //Set Error/Validation Messages on form
        this.setMessageOnForm(this.caForm);

        // Back/Next Button Click events communication
        //debugger;
        this.paginationMessageSubscription = this.cms.getbtnClickNotification().subscribe(message => {
            //debugger;
            this.paginationMessage = (<any>message).text;
            Object.keys(this.formGroupMetadata).forEach((index) => {
                //debugger;
                if ((<any>this.formGroupMetadata)[index].groupName == this.paginationMessage.formGroupName) {
                    //debugger;
                    if (this.paginationMessage.nextBtnClicked) {
                        if ((<any>this.formGroupMetadata)[Number(index) + 1]) {
                            //debugger;
                            (<any>this.formGroupMetadata)[Number(index) + 1].hidden = false;  // SHOW - HIDE of CARD component
                            (<any>this.formGroupMetadata)[Number(index) + 1].paginationValidation.paginationValidation =  // SHOW - HIDE of PAGINATION component
                                new PaginationValidation(true, true, false, false, true, true, false, true, (<any>this.formGroupMetadata)[Number(index) + 1].groupName);
                        }
                    } else {
                        if ((<any>this.formGroupMetadata)[Number(index) - 1]) {
                            //debugger;
                            (<any>this.formGroupMetadata)[Number(index) - 1].hidden = false;
                        }
                    }
                    (<any>this.formGroupMetadata)[index].hidden = true;
                }
            });
            //debugger;
            //this.cms.clearSubjectMessage();
        });

        //Get Initial Lookup Columns from Database
        this.hds.getApplicationLookups(AppConfigurableSettings.DATA_API +'/GetApplicationAllLookups').subscribe(
            data => {
                //debugger;
                this.stdAppDataLkp = new StudentApplicationDataLookup();
                this.stdAppDataLkp.course = data.d.course;
                this.stdAppDataLkp.campus = data.d.campus;
                this.stdAppDataLkp.courseCampus = data.d.courseCampus;
                this.stdAppDataLkp.country = data.d.country;
                this.stdAppDataLkp.vrt_australiancitizenshipresidency = data.d.vrt_australiancitizenshipresidency;
                this.stdAppDataLkp.vrt_aboriginalortorresstraitislander = data.d.vrt_aboriginalortorresstraitislander;
                this.stdAppDataLkp.txtQualification = data.d.txtQualification;
                this.stdAppDataLkp.state = data.d.state;
                this.stdAppDataLkp.idProof = data.d.idProof;
                this.stdAppDataLkp.whatBroughtYouHere = data.d.whatBroughtYouHere;
                //debugger;
                //console.log('opiGroupValid :-  '+this.opiGroupValid);
                //this.formValidation = data.formValidation;
            },
            err => { debugger; console.log('get error: ', err) }
        ); 

    }

    //control validation
    private formValidation: Array<any> = [];
    public fe: FormElements = new FormElements();
    private formControlNames = {
        vrt_title: {
            required: 'Please select a Title',
        },
        firstName: {
            required: 'Please enter First Name',
        },
        lastname: {
            required: "Please enter Last Name"
        },
        mobilephone: {
            required: "Please enter Mobile Phone"
        },
        emailaddress1: {
            required: "Please enter email address",
            pattern: "Please enter correct email address"
        },
        txtConfirmEmail: {
            required: "Please enter email address again",
            match: "Please enter the same email address as mentioned above"
        },
        birthdate: {
            required: "Please enter Date of Birth",
            pattern: "Please enter Birthdate in correct in dd/mm/yyyy format"
        },
        studentGender: {
            required: "Please select the appropriate Gender",
        },
        address1_postalcode: {
            required: "Please Enter Post Code",
        },
        vrt_whatbroughtyoutothekanganinstitutewebsite: {
            required: "Please select the appropriate reason for choosing the our TAFE",
        },
        vrt_kibtstudentidnumber: {
            required: "Please Enter Previous Student Number",
        }
    };

    private formGroupMetadata: Array<IFormGroupMetadata> = [
        {
            groupIndex: 0,
            groupName: 'piGroup',
            grouptitle: 'Before You Start',
            hidden: false,
            paginationValidation: { 'paginationValidation': new PaginationValidation(false, true, true, true, true, true, false, true, 'piGroup') }
        },
        {
            groupIndex: 1,
            groupName: 'opiGroup',
            grouptitle: 'Personal Information',
            hidden: true,
            paginationValidation: { 'paginationValidation': new PaginationValidation(true, true, false, false, true, true, false, true, 'opiGroup') }
        },
        {
            groupIndex: 1,
            groupName: 'resGroup',
            grouptitle: 'Residency and cultural diversity',
            hidden: true,
            paginationValidation: { 'paginationValidation': new PaginationValidation(true, true, false, false, true, true, false, true, 'resGroup') }
        },
    ];

    setMessageOnForm(c: FormGroup): void {
        Object.keys(c.controls).forEach((name) => {
            let formControl = c.controls[name];
            if (formControl instanceof FormGroup) {
                this.setMessageOnForm(formControl);
                this.checkErrorOnGroupSubscribeRef = formControl.valueChanges.subscribe(value => {
                //formControl.valueChanges.subscribe(value => {
                    this.checkErrorOnControl(formControl, name);
                }
                );
            } else {
                if (formControl instanceof FormArray) {
                    this.setMessageOnForm(<FormGroup>formControl.controls[0]);
                }
                this.setMessageOnControlSubscribeRef = formControl.valueChanges.debounceTime(1000).subscribe(value => {
                //formControl.valueChanges.debounceTime(1000).subscribe(value => {
                    this.setMessageOnControl(formControl, name);
                }
                );
                this.checkErrorOnControlSubscribeRef = formControl.valueChanges.subscribe(value => {
                //formControl.valueChanges.subscribe(value => {
                    this.checkErrorOnControl(formControl, name);
                }
                );
            }
        });
    }
    setMessageOnControl(c: AbstractControl, controlName: string): void {
        Object.keys(this.formControlNames).forEach((name) => //formControlNames
        {
            if (controlName === name) {
                (<any>this.fe)[name] = "";
                if ((c.touched || c.dirty) && c.errors) {
                    (<any>this.fe)[name] = Object.keys(c.errors).map(key =>
                        (<any>this.formControlNames)[name][key]).join(' ');
                }
            }
        });
    }
    checkErrorOnControl(c: AbstractControl, controlName: string): void {
        //debugger;
        Object.keys(this.formGroupMetadata).forEach((index) => {
            if ((<any>this.formGroupMetadata)[index].groupName == controlName && controlName == 'piGroup') {
                if (c.valid == true) {
                    (<any>this.formGroupMetadata)[index].paginationValidation.paginationValidation = new PaginationValidation(false, false, true, true, true, true, false, true, controlName);
                } else {
                    (<any>this.formGroupMetadata)[index].paginationValidation.paginationValidation = new PaginationValidation(false, true, true, true, true, true, false, true, controlName);
                }
            }
            else {
                if (c.valid == true) {
                    (<any>this.formGroupMetadata)[index].paginationValidation.paginationValidation = new PaginationValidation(true, true, false, false, true, true, false, false, controlName);
                } else {
                    (<any>this.formGroupMetadata)[index].paginationValidation.paginationValidation = new PaginationValidation(true, true, false, false, true, true, false, true, controlName);
                }
            }
        });


      // if (controlName == 'piGroup') {
      //     //debugger;
      //     this.piGroupValid = c.valid;
      //     //this.cms.sendFormgroupValidNotification(new FormGroupValid('piGroup', c.valid));
      //     if (c.valid == true) {
      //         //debugger;
      //         //console.log((<any>this.formGroupMetadata)[0].paginationValidation.paginationValidation);
      //         (<any>this.formGroupMetadata)[0].paginationValidation.paginationValidation = new PaginationValidation(false, false, true, true, true, true, false, true, 'piGroup');
      //         //console.log((<any>this.formGroupMetadata)[0].paginationValidation.paginationValidation);
      //     } else {
      //         (<any>this.formGroupMetadata)[0].paginationValidation.paginationValidation = new PaginationValidation(false, true, true, true, true, true, false, true, 'piGroup');
      //     }
      // }
      // else if (controlName == 'opiGroup') {
      //     this.opiGroupValid = c.valid;
      // }
    }

    paginationBtnEvtNotify(formGroupDetails: FormGroupDetails): void {   //////   NOT USED -- REPLACED WITH SERIVCE
        //debugger;
        Object.keys(this.formGroupMetadata).forEach((index) => {
            //debugger;
            if ((<any>this.formGroupMetadata)[index].groupName == formGroupDetails.formGroupName) {
                //debugger;
                if (formGroupDetails.nextBtnClicked) {
                    if ((<any>this.formGroupMetadata)[Number(index) + 1]) {
                        //debugger;
                        (<any>this.formGroupMetadata)[Number(index) + 1].hidden = false;
                    }
                } else {
                    if ((<any>this.formGroupMetadata)[Number(index) - 1]) {
                        //debugger;
                        (<any>this.formGroupMetadata)[Number(index) - 1].hidden = false;
                    }
                }
                (<any>this.formGroupMetadata)[index].hidden = true;
            }
        });
    }

    // Conditional Validation function - vrt_kibtstudentidnumber
    sendNotificationToVrt_kibtstudentidnumber(notifyVia: number): void {
        //debugger;
        const control = this.caForm.get('opiGroup.vrt_kibtstudentidnumber');
        if (notifyVia == 1) {
            control!.setValidators(Validators.required);
        } else {
            control!.clearValidators();
        }
        control!.updateValueAndValidity();
        //debugger;
        this.cms.sendVrt_kibtstudentidnumberNotification(notifyVia);
    }

    ngOnDestroy() {
        this.setMessageOnControlSubscribeRef.unsubscribe();
        this.checkErrorOnControlSubscribeRef.unsubscribe();
        this.checkErrorOnGroupSubscribeRef.unsubscribe();       
        this.paginationMessageSubscription.unsubscribe();
        this.vrt_studiedatkanganinstitutebendigotafebeforeSubscription.unsubscribe();
        this.cms.clearSubjectMessage();
    }

}