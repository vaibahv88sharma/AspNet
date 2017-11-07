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
    private applyUSISubscription: Subscription;
    //txtQualificationSubscription: Subscription;

    private hasUSIHiddenControls: boolean;
    private applyUSIHiddenControls: boolean;
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

                this.usiGroupForm.controls['streetNumber'].reset();
                this.usiGroupForm.controls['streetName'].reset();
                this.usiGroupForm.controls['city'].reset();
                this.usiGroupForm.controls['state'].reset();
                this.usiGroupForm.controls['vrt_CityorTownofBirth'].reset();
                this.usiGroupForm.controls['vrt_CountryofBirth'].reset();
                this.usiGroupForm.controls['vrt_CountryofResidence'].reset();
                this.usiGroupForm.controls['idProofType'].reset();
                this.usiGroupForm.controls['idProof'].reset();
            }
        });

        // applyUSI Radio Button Click events communication
        this.applyUSISubscription = this.cms.getApplyUSINotification().subscribe(message => {
            //this.studentNumberHidden = (<any>message).text == 1 ? false : true;
            //this.opiGroupForm.patchValue({
            //    vrt_kibtstudentidnumber: (<any>message).text == 1 ? (<any>this.opiGroupForm.get('vrt_kibtstudentidnumber')).value : ""
            //});
            if ((<any>message).text == 0) {
                this.usiGroupForm.controls['streetNumber'].reset();
                this.usiGroupForm.controls['streetName'].reset();
                this.usiGroupForm.controls['city'].reset();
                this.usiGroupForm.controls['state'].reset();
                this.usiGroupForm.controls['vrt_CityorTownofBirth'].reset();
                this.usiGroupForm.controls['vrt_CountryofBirth'].reset();
                this.usiGroupForm.controls['vrt_CountryofResidence'].reset();
                this.usiGroupForm.controls['idProofType'].reset();
                this.usiGroupForm.controls['idProof'].reset();
            }
        });
    }

    ngDoCheck() {
        //debugger;
        if (this.usiGroupForm.controls['hasUSI']!.value) {
            this.hasUSIHiddenControls = this.usiGroupForm.controls['hasUSI']!.value == 1 ? true : false;
            //console.log(this.hasUSIHiddenControls);
        }
        if (this.usiGroupForm.controls['applyUSI']!.value) {
            this.applyUSIHiddenControls = this.usiGroupForm.controls['applyUSI']!.value == 1 ? true : false;
        } else {
            this.applyUSIHiddenControls = false;
        }
        //console.log(this.usiGroupForm.parent.get('resGroup.vrt_australiancitizenshipresidency')!.value);//resGroup
    }
    ngAfterViewInit() {
    }
    ngOnDestroy() {
        this.hasUSISubscription.unsubscribe();
        this.applyUSISubscription.unsubscribe();
    }


}