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

@Component({
    selector: 'other-personalinfo',
    templateUrl: './other-personalinfo.component.html',
    styleUrls: ['./other-personalinfo.component.css']
})
export class OtherPersonalinfoComponent
    implements DoCheck, AfterViewInit
{
    @Input('opiGrp')
    public opiGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;

    @Input('opiGrpIsVld')
    public opiGroupIsValid: boolean;

    @Input('grpNm')
    private groupName: string;

    @Output() paginationBtnEvt = new EventEmitter<FormGroupDetails>();

    private opiPaginationValidation: PaginationValidation;
    private paginationBtnEvnt: PaginationButtonEvent = new PaginationButtonEvent();

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
    }

    paginationButtonEvent(formGroupDetails: FormGroupDetails): void {
        //debugger;
        this.paginationBtnEvt.emit(formGroupDetails);
    }   

}