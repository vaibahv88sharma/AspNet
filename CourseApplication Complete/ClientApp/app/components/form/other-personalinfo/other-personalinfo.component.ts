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
import { HomeDataService } from '../../shared/services/home-data.service';
import { PaginationValidation, PaginationButtonEvent } from '../../shared/model/pagination-validation';
import { CommonMethods } from '../../shared/public/common-methods';
import { PaginationComponent } from '../../shared/pagination/pagination.component';
import { StudentApplicationDataLookup } from '../../shared/model/data-binding';
import { ComponentMessageService } from "../../shared/services/component-message.service";
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'other-personalinfo',
    templateUrl: './other-personalinfo.component.html',
    styleUrls: ['./other-personalinfo.component.css']
})
export class OtherPersonalinfoComponent
    implements DoCheck, AfterViewInit, OnInit, OnDestroy
{
    @Input('opiGrp')
    public opiGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;

    @Input('opiGrpIsVld')
    public opiGroupIsValid: boolean;

    @Input('grpNm')
    private groupName: string;

    @Input('stdntAppDataLkp')
    private studentApplicationDataLookup: StudentApplicationDataLookup;

    //@Input('studentNumberhide') studentNumberhidden: boolean = true;


    @Output() paginationBtnEvt = new EventEmitter<FormGroupDetails>();

    private opiPaginationValidation: PaginationValidation;
    private paginationBtnEvnt: PaginationButtonEvent = new PaginationButtonEvent();
    //private vrt_whatbroughtyoutothekanganinstitutewebsiteMessage: number;
    private studentNumberHidden: boolean;

    vrt_kibtstudentidnumberSubscription: Subscription;

    constructor(private cms: ComponentMessageService) {
    }

    ngOnInit(): void {
        //debugger;
        this.studentNumberHidden = true;

        // vrt_whatbroughtyoutothekanganinstitutewebsite Radio Button Click events communication
        this.vrt_kibtstudentidnumberSubscription = this.cms.getVrt_kibtstudentidnumberNotification().subscribe(message => {
            this.studentNumberHidden = (<any>message).text == 1 ? false : true;
            //this.opiGroupForm.patchValue({
            //    vrt_kibtstudentidnumber: (<any>message).text == 1 ? (<any>this.opiGroupForm.get('vrt_kibtstudentidnumber')).value : ""
            //});
            if ((<any>message).text == 0) {
                //debugger;
                this.opiGroupForm.controls['vrt_kibtstudentidnumber'].reset();
            }
        });

    }

    ngDoCheck() {
        if (this.opiGroupIsValid) {
            //debugger;
            this.opiPaginationValidation = new PaginationValidation(true, true, false, false, true, true, false, false, this.groupName);
        } else {
            //debugger;
            this.opiPaginationValidation = new PaginationValidation(true, true, false, false, true, true, false, true, this.groupName);
        }
    }
    ngAfterViewInit() {
        //debugger;
        //this.opiPaginationValidation = new PaginationValidation(true, true, false, false, true, true, false, true, this.groupName);
        //debugger;
        //console.log(this.studentApplicationDataLookup);
    }
    ngOnDestroy() {
        this.vrt_kibtstudentidnumberSubscription.unsubscribe();
    }


    paginationButtonEvent(formGroupDetails: FormGroupDetails): void {
        //debugger;
        this.paginationBtnEvt.emit(formGroupDetails);
    }   

}