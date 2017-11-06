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
    selector: 'prev-qual',
    templateUrl: './prev-qual.component.html',
    styleUrls: ['./prev-qual.component.css']
})
export class PreviousQualificationComponent
    implements DoCheck, AfterViewInit, OnInit, OnDestroy
{
    @Input('pqGrp')
    public pqGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;

    //@Input('opiGrpIsVld')
    //public opiGroupIsValid: boolean;

    //@Input('grpNm')
    //private groupName: string;

    @Input('stdntAppDataLkp')
    private studentApplicationDataLookup: StudentApplicationDataLookup;

    txtQualificationSubscription: Subscription;

    private txtQualificationHidden: boolean;
    //@Input('studentNumberhide') studentNumberhidden: boolean = true;



    //private opiPaginationValidation: PaginationValidation;
    //private paginationBtnEvnt: PaginationButtonEvent = new PaginationButtonEvent();
    //private vrt_whatbroughtyoutothekanganinstitutewebsiteMessage: number;
    //private studentNumberHidden: boolean;


    constructor(private cms: ComponentMessageService) {
    }

    ngOnInit(): void {
        //this.txtQualificationHidden = false;
        // vrt_whatbroughtyoutothekanganinstitutewebsite Radio Button Click events communication
        this.txtQualificationSubscription = this.cms.getTxtQualificationNotification().subscribe(message => {
            //this.studentNumberHidden = (<any>message).text == 1 ? false : true;
            //this.opiGroupForm.patchValue({
            //    vrt_kibtstudentidnumber: (<any>message).text == 1 ? (<any>this.opiGroupForm.get('vrt_kibtstudentidnumber')).value : ""
            //});
            //debugger;
            if ((<any>message).text == 0) {
                //debugger;
                //this.pqGroupForm.controls['txtQualification'].reset();
                //console.log((<FormGroup>this.pqGroupForm.get('txtQualification'))!.controls);
                //(<FormGroup>formControl.controls[0]).controls

                Object.keys((<FormGroup>this.pqGroupForm.get('txtQualification'))!.controls).forEach((name) => //formControlNames
                {
                    //debugger;
                    (<FormGroup>this.pqGroupForm.get('txtQualification'))!.controls[name].setValue(false);
                    //(<FormGroup>this.pqGroupForm.get('txtQualification'))!.controls[name].reset();
                    //console.log((<FormGroup>this.pqGroupForm.get('txtQualification'))!.controls[name]);
                    //this.pqGroupForm.get(name).
                    //console.log(this.pqGroupForm.get(name));
                });
                //this.txtQualificationHidden = true;
            }
            //else {
            //    this.txtQualificationHidden = false;
            //}
        });

    }

    ngDoCheck() {
        //debugger;
        if (this.pqGroupForm.controls['vrt_successfullycompletedqualifications']!.value) {
            this.txtQualificationHidden = this.pqGroupForm.controls['vrt_successfullycompletedqualifications']!.value == 1 ? true : false;
        }
        //if (this.usiGroupForm.controls['hasUSI']!.value) {
        //    this.hasUSIHiddenControls = this.usiGroupForm.controls['hasUSI']!.value == 1 ? true : false;
        //    //console.log(this.hasUSIHiddenControls);
        //}
    }
    ngAfterViewInit() {
    }
    ngOnDestroy() {
        this.txtQualificationSubscription.unsubscribe();
    }


}