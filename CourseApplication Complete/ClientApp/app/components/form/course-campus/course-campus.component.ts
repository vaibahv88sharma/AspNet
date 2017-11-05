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
    selector: 'course-campus',
    templateUrl: './course-campus.component.html',
    styleUrls: ['./course-campus.component.css']
})
export class CourseCampusComponent
    implements DoCheck, AfterViewInit, OnInit, OnDestroy
{
    @Input('ccGrp')
    public ccGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;

    //@Input('opiGrpIsVld')
    //public opiGroupIsValid: boolean;

    //@Input('grpNm')
    //private groupName: string;

    @Input('stdntAppDataLkp')
    private studentApplicationDataLookup: StudentApplicationDataLookup;

    //txtQualificationSubscription: Subscription;

    //private txtQualificationHidden: boolean;
    //@Input('studentNumberhide') studentNumberhidden: boolean = true;



    //private opiPaginationValidation: PaginationValidation;
    //private paginationBtnEvnt: PaginationButtonEvent = new PaginationButtonEvent();
    //private vrt_whatbroughtyoutothekanganinstitutewebsiteMessage: number;
    //private studentNumberHidden: boolean;


    constructor(private cms: ComponentMessageService) {
    }

    ngOnInit(): void {
    }

    ngDoCheck() {
        //debugger;
       // console.log(this.ccGroupForm.get('vrt_course'));
        //console.log(this.pqGroupForm);
    }
    ngAfterViewInit() {
    }
    ngOnDestroy() {
        //this.txtQualificationSubscription.unsubscribe();
    }

    vrt_courseClick(e: Event): void {
        if (this.ccGroupForm.get('vrt_course')!.value) {
            this.ccGroupForm.get('txtCampus')!.reset({ value: '', disabled: false });
        }
        //console.log(this.ccGroupForm.get('vrt_course'));
    }

}