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
    implements DoCheck, OnInit, OnDestroy
{
    @Input('opiGrp')
    public opiGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;

    @Input('stdntAppDataLkp')
    public studentApplicationDataLookup: StudentApplicationDataLookup;

    @Output() paginationBtnEvt = new EventEmitter<FormGroupDetails>();

    private opiPaginationValidation: PaginationValidation;
    private paginationBtnEvnt: PaginationButtonEvent = new PaginationButtonEvent();
    public studentNumberHidden: boolean;

    vrt_kibtstudentidnumberSubscription: Subscription;

    constructor(private cms: ComponentMessageService) {
    }

    ngOnInit(): void {

        // vrt_whatbroughtyoutothekanganinstitutewebsite Radio Button Click events communication
        this.vrt_kibtstudentidnumberSubscription = this.cms.getVrt_kibtstudentidnumberNotification().subscribe(message => {
            if ((<any>message).text == 0) {
                this.opiGroupForm.controls['vrt_kibtstudentidnumber'].reset();
            }
        });

    }

    ngDoCheck() {
        if (this.opiGroupForm.controls['vrt_studiedatkanganinstitutebendigotafebefore']!.value) {
            this.studentNumberHidden = this.opiGroupForm.controls['vrt_studiedatkanganinstitutebendigotafebefore']!.value == 1 ? true : false;
        }
    }
    ngOnDestroy() {
        this.vrt_kibtstudentidnumberSubscription.unsubscribe();
    }



}