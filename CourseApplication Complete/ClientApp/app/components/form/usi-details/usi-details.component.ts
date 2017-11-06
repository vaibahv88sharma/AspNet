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
    selector: 'usi-details',
    templateUrl: './usi-details.component.html',
    styleUrls: ['./usi-details.component.css']
})
export class UsiDetailsComponent
    implements DoCheck, AfterViewInit, OnInit, OnDestroy
{
    @Input('usiGrp')
    public usiGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;

    //@Input('opiGrpIsVld')
    //public opiGroupIsValid: boolean;

    //@Input('grpNm')
    //private groupName: string;

    @Input('stdntAppDataLkp')
    private studentApplicationDataLookup: StudentApplicationDataLookup;

    private hasUSISubscription: Subscription;

    //txtQualificationSubscription: Subscription;

    private hasUSIHiddenControls: boolean;
    //@Input('studentNumberhide') studentNumberhidden: boolean = true;



    //private opiPaginationValidation: PaginationValidation;
    //private paginationBtnEvnt: PaginationButtonEvent = new PaginationButtonEvent();
    //private vrt_whatbroughtyoutothekanganinstitutewebsiteMessage: number;
    //private studentNumberHidden: boolean;


    constructor(private cms: ComponentMessageService) {
    }

    ngOnInit(): void {
        //this.hasUSIHiddenControls = false;
        // hasUSI Radio Button Click events communication
        this.hasUSISubscription = this.cms.getHasUSINotification().subscribe(message => {
            //this.studentNumberHidden = (<any>message).text == 1 ? false : true;
            //this.opiGroupForm.patchValue({
            //    vrt_kibtstudentidnumber: (<any>message).text == 1 ? (<any>this.opiGroupForm.get('vrt_kibtstudentidnumber')).value : ""
            //});
            if ((<any>message).text == 0) {
                //this.hasUSIHiddenControls = false;
                //debugger;
                this.usiGroupForm.controls['vrt_uniquestudentidentifier'].reset();
                this.usiGroupForm.controls['vrt_permissiontocheckfororcreateausi'].reset();
            } else {
                //this.hasUSIHiddenControls = true;
                this.usiGroupForm.controls['applyUSI'].reset();
            }
        });
    }

    ngDoCheck() {
        //debugger;
        if (this.usiGroupForm.controls['hasUSI']!.value) {
            this.hasUSIHiddenControls = this.usiGroupForm.controls['hasUSI']!.value == 1 ? true : false;
            //console.log(this.hasUSIHiddenControls);
        }
    }
    ngAfterViewInit() {
    }
    ngOnDestroy() {
        this.hasUSISubscription.unsubscribe();
    }


}