import {
    Component,
    OnChanges,
    OnInit,
    DoCheck,
    AfterContentInit,
    AfterContentChecked,
    AfterViewInit,
    AfterViewChecked,
    OnDestroy,
    Input,
    ViewChild,
    ContentChild,
    EventEmitter,
    Output,
} from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';  //Reactive Forms
import 'rxjs/add/operator/debounceTime';
import { FormElements, FormGroupDetails } from '../../shared/model/form-elements';
import { EmailControlsComponent } from './email-controls/email-controls.component';
import { HomeDataService } from '../../shared/services/home-data.service';
import { PaginationValidation, PaginationButtonEvent } from '../../shared/model/pagination-validation';
import { CommonMethods } from '../../shared/public/common-methods';
import { PaginationComponent } from '../../shared/pagination/pagination.component';
import { AppConfigurableSettings } from '../../shared/services/app-configurable.settings';//'./app-configurable.settings';

@Component({
    selector: 'app-personalinfo',
    templateUrl: './personalinfo.component.html',
    styleUrls: ['./personalinfo.component.css']
})
export class PersonalinfoComponent
    //implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy
    implements DoCheck, AfterViewChecked, AfterViewInit {

    @Input('piGrp')
    public piGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;
    //public fe: FormElements = new FormElements();

    @Input('piGrpIsVld')
    public piGroupIsValid: boolean;

    @Output() paginationBtnEvt = new EventEmitter<FormGroupDetails>();

    @Input('grpNm')
    private groupName: string;


    //@ViewChild(EmailControlsComponent) emailcontrol: EmailControlsComponent;
    //@ViewChild(PaginationComponent) piPaginationComponent: PaginationComponent;



    private piPaginationValidation: PaginationValidation;
    private isUnderstand: boolean;
    private paginationBtnEvnt: PaginationButtonEvent = new PaginationButtonEvent();

    constructor(private hds: HomeDataService) {
        //cr = new CompanyRegister();
    }



    //ngOnChanges() {
    //    debugger;
    //    console.log(this.feError);
    //    }
    ngOnInit() {
        //debugger;
        //console.log(this.groupName);
        //this.piPaginationValidation = new PaginationValidation();
        //this.piPaginationValidation = new PaginationValidation(false, true, true, true, true, true, false, true);
        this.hds.getApplicationLookups(AppConfigurableSettings.DATA_API +'/GetApplicationAllLookups').subscribe(
            data => {
                debugger;
                //this.formValidation = data.formValidation;
            },
            err => { debugger; console.log('get error: ', err) }
        ); 
    }

    ngDoCheck() {
        //debugger;
        //let a = console.log(CommonMethods.formHasError(this.piGroupForm));
        //let a = CommonMethods.formHasError(this.piGroupForm);
        //if (CommonMethods.formHasError(this.piGroupForm, false)) {
        //    //debugger;
        //    this.piPaginationValidation = new PaginationValidation(false, true, true, true, true, true, false, true);
        //} else {
        //    debugger;
        //    this.piPaginationValidation = new PaginationValidation(false, false, true, true, true, true, false, true);
        //}

        //Pagination Component
        if (this.piGroupIsValid) {
            //debugger;
            this.piPaginationValidation = new PaginationValidation(false, false, true, true, true, true, false, true, this.groupName);
            if (this.isUnderstand) {
                this.piPaginationValidation = new PaginationValidation(false, false, true, true, true, true, false, false, this.groupName);
            }

        } else {
            //debugger;
            this.piPaginationValidation = new PaginationValidation(false, true, true, true, true, true, false, true, this.groupName);
        }
        //debugger;
        //console.log(this.nextButtonClick);
        //if (this.nextButtonClick.buttonClicked) {
        //    debugger;
        //    this.nextButtonClick.buttonClicked = false;
        //}
    }

    //    ngAfterContentInit() {
    //    }
    //    ngAfterContentChecked() {
    //    }
    ngAfterViewInit() {
        //debugger;
        //this.piPaginationValidation = new PaginationValidation(false, true, true, true, true, true, false, true, this.groupName);
    }

    ngAfterViewChecked() {
        //debugger;
        if (!(this.piGroupForm.get('emailGroup.emailaddress1')!.errors)
            && !(this.piGroupForm.get('emailGroup.txtConfirmEmail')!.errors)
            && !(this.piGroupForm.get('emailGroup')!.errors)
        ) {
            //this.hds.postEmailValidate(this.piGroupForm.get('emailGroup.emailaddress1')!.value).subscribe(
            //    data => {
            //        debugger;
            //        //this.formValidation = data.formValidation;
            //    },
            //    err => { debugger; console.log('get error: ', err) }
            //); 

            //this.hds.postEmailValidate(this.piGroupForm.get('emailGroup.emailaddress1')!.value);
            //console.log(a);
        }
        //console.log("EmailControlsComponent: " + this.piGroupForm.get('emailGroup.emailaddress1'));
    }

    isUnderstandChangeEvent(isUnderstand: boolean): void {
        //debugger;
        this.isUnderstand = isUnderstand;
    }
    //paginationBtnClickEvnt(buttonClick: PaginationButtonEvent): void {
    //    debugger;
    //    this.paginationBtnEvnt = buttonClick;
    //    //this.paginationButtonEvent.emit(ev);
    //}
    paginationButtonEvent(formGroupDetails: FormGroupDetails): void {
        //debugger;
        //this.paginationBtnEvt = formGroupDetails;

        this.paginationBtnEvt.emit(formGroupDetails);
    }

    //    ngOnDestroy() {
    //    }


}