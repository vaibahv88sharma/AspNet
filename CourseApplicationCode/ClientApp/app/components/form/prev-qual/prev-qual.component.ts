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
    selector: 'prev-qual',
    templateUrl: './prev-qual.component.html',
    styleUrls: ['./prev-qual.component.css']
})
export class PreviousQualificationComponent
    implements DoCheck, OnInit, OnDestroy
{
    @Input('pqGrp')
    public pqGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;

    @Input('stdntAppDataLkp')
    public studentApplicationDataLookup: StudentApplicationDataLookup;

    txtQualificationSubscription: Subscription;

    public txtQualificationHidden: boolean;

    constructor(private cms: ComponentMessageService) {
    }

    ngOnInit(): void {
        // vrt_whatbroughtyoutothekanganinstitutewebsite Radio Button Click events communication
        this.txtQualificationSubscription = this.cms.getTxtQualificationNotification().subscribe(message => {
            if ((<any>message).text == 0) {
                Object.keys((<FormGroup>this.pqGroupForm.get('txtQualification'))!.controls).forEach((name) =>
                {
                    (<FormGroup>this.pqGroupForm.get('txtQualification'))!.controls[name].setValue(false);
                });
            }
        });

    }

    ngDoCheck() {
        if (this.pqGroupForm.controls['vrt_successfullycompletedqualifications']!.value) {
            this.txtQualificationHidden = this.pqGroupForm.controls['vrt_successfullycompletedqualifications']!.value == 1 ? true : false;
        }
    }
    ngOnDestroy() {
        this.txtQualificationSubscription.unsubscribe();
    }


}