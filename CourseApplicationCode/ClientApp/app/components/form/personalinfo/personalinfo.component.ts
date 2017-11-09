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
import { PaginationComponent } from '../../shared/pagination/pagination.component';
import { AppConfigurableSettings } from '../../shared/services/app-configurable.settings';//'./app-configurable.settings';
import { StudentApplicationDataLookup } from '../../shared/model/data-binding';

@Component({
    selector: 'app-personalinfo',
    templateUrl: './personalinfo.component.html',
    styleUrls: ['./personalinfo.component.css']
})
export class PersonalinfoComponent{

    @Input('piGrp')
    public piGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;

    public studentApplicationDataLookup: StudentApplicationDataLookup;

    private piPaginationValidation: PaginationValidation;
    private isUnderstand: boolean;
    private paginationBtnEvnt: PaginationButtonEvent = new PaginationButtonEvent();

    constructor(private hds: HomeDataService) {
    }


}