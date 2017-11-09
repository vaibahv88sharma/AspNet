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
    selector: 'res-aboriginal',
    templateUrl: './res-aboriginal.component.html',
    styleUrls: ['./res-aboriginal.component.css']
})
export class ResidencyAboriginalComponent
{
    @Input('resGrp')
    public resGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;

    @Input('stdntAppDataLkp')
    public studentApplicationDataLookup: StudentApplicationDataLookup;

    constructor() {
    }


}