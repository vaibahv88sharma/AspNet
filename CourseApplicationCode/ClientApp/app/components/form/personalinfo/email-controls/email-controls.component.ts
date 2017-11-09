import {
    Component, OnInit, Input, OnChanges, DoCheck, AfterViewChecked,
} from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';  //Reactive Forms
import { FormElements } from "../../../shared/model/form-elements";

@Component({
    selector: 'email-controls',
    templateUrl: './email-controls.component.html',
    styleUrls: ['./email-controls.component.css']
})
export class EmailControlsComponent {

    @Input('emailGrp')
    public emailGroupForm: FormGroup;

    @Input('egErr')
    public egError: FormElements;

    constructor() { }

}
