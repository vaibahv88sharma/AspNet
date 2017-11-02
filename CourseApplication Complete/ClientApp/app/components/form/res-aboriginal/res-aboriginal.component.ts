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
    selector: 'res-aboriginal',
    templateUrl: './res-aboriginal.component.html',
    styleUrls: ['./res-aboriginal.component.css']
})
export class ResidencyAboriginalComponent
    implements DoCheck, AfterViewInit, OnInit, OnDestroy
{
    @Input('resGrp')
    public resGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;

    //@Input('opiGrpIsVld')
    //public opiGroupIsValid: boolean;

    //@Input('grpNm')
    //private groupName: string;

    @Input('stdntAppDataLkp')
    private studentApplicationDataLookup: StudentApplicationDataLookup;

    //@Input('studentNumberhide') studentNumberhidden: boolean = true;



    //private opiPaginationValidation: PaginationValidation;
    //private paginationBtnEvnt: PaginationButtonEvent = new PaginationButtonEvent();
    //private vrt_whatbroughtyoutothekanganinstitutewebsiteMessage: number;
    //private studentNumberHidden: boolean;


    constructor(private cms: ComponentMessageService) {
    }

    ngOnInit(): void {
        //debugger;
        //console.log(this.studentApplicationDataLookup);
    }

    ngDoCheck() {
        //debugger;
      // if (this.opiGroupIsValid) {
      //     //debugger;
      //     this.opiPaginationValidation = new PaginationValidation(true, true, false, false, true, true, false, false, this.groupName);
      // } else {
      //     //debugger;
      //     this.opiPaginationValidation = new PaginationValidation(true, true, false, false, true, true, false, true, this.groupName);
      // }
    }
    ngAfterViewInit() {
        //debugger;
        //this.opiPaginationValidation = new PaginationValidation(true, true, false, false, true, true, false, true, this.groupName);
        //debugger;
        //console.log(this.studentApplicationDataLookup);
    }
    ngOnDestroy() {
    }


}