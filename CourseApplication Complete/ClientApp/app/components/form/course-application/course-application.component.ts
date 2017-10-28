﻿import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';  //Reactive Forms
import 'rxjs/add/operator/debounceTime';
import { CardLayout } from '../../shared/model/card-layout';
import { FormElements, FormGroupDetails, IFormGroupMetadata } from '../../shared/model/form-elements';
import { Subscription } from 'rxjs/Subscription';
import { ComponentMessageService } from '../../shared/services/component-message.service';
import { StudentApplicationDataLookup } from '../../shared/model/data-binding';
import { HomeDataService } from '../../shared/services/home-data.service';
import { AppConfigurableSettings } from '../../shared/services/app-configurable.settings';//'./app-configurable.settings';

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

    private studentApplicationDataLookup: StudentApplicationDataLookup; // Loopup values from databse

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
                birthdate: ['', [Validators.required]]
            }),
            resGroup: this.fb.group({
                residency: ['', [Validators.required]]
            })
        });

        //Set Error/Validation Messages on form
        this.setMessageOnForm(this.caForm);

        // Back/Next Button Click events communication
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
        });

        //Get Initial Lookup Columns from Database
        this.hds.getApplicationLookups(AppConfigurableSettings.DATA_API +'/GetApplicationAllLookups').subscribe(
            data => {
                //debugger;
                this.studentApplicationDataLookup = new StudentApplicationDataLookup();
                this.studentApplicationDataLookup.course = data.d.course;
                this.studentApplicationDataLookup.campus = data.d.campus;
                this.studentApplicationDataLookup.courseCampus = data.d.courseCampus;
                this.studentApplicationDataLookup.country = data.d.country;
                this.studentApplicationDataLookup.vrt_australiancitizenshipresidency = data.d.vrt_australiancitizenshipresidency;
                this.studentApplicationDataLookup.vrt_aboriginalortorresstraitislander = data.d.vrt_aboriginalortorresstraitislander;
                this.studentApplicationDataLookup.txtQualification = data.d.txtQualification;
                this.studentApplicationDataLookup.state = data.d.state;
                this.studentApplicationDataLookup.idProof = data.d.idProof;
                this.studentApplicationDataLookup.whatBroughtYouHere = data.d.whatBroughtYouHere;
                console.log(this.studentApplicationDataLookup);
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
        }

    };

    private formGroupMetadata: Array<IFormGroupMetadata> = [
        {
            groupIndex: 0,
            groupName: 'piGroup',
            grouptitle: 'Before You Start',
            hidden: false,
        },
        {
            groupIndex: 1,
            groupName: 'opiGroup',
            grouptitle: 'Personal Information',
            hidden: true,
        },
        {
            groupIndex: 1,
            groupName: 'resGroup',
            grouptitle: 'Residency and cultural diversity',
            hidden: true,
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
        if (controlName == 'piGroup') {
            //debugger;
            this.piGroupValid = c.valid;
        }
        else if (controlName == 'opiGroup') {
            this.opiGroupValid = c.valid;
        }
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

    ngOnDestroy() {
        this.setMessageOnControlSubscribeRef.unsubscribe();
        this.checkErrorOnControlSubscribeRef.unsubscribe();
        this.checkErrorOnGroupSubscribeRef.unsubscribe();       
        this.paginationMessageSubscription.unsubscribe();
    }

}