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
} from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';  //Reactive Forms
import 'rxjs/add/operator/debounceTime';
import { FormElements } from '../../shared/model/form-elements';


@Component({
    selector: 'app-personalinfo',
    templateUrl: './personalinfo.component.html',
    styleUrls: ['./personalinfo.component.css']
})
export class PersonalinfoComponent
    //implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy
    implements DoCheck, AfterViewChecked
{

    @Input('piGrp')
    public piGroupForm: FormGroup;

    @Input('feErr')
    public feError: FormElements;
    //public fe: FormElements = new FormElements();


        
    //ngOnChanges() {
    //    debugger;
    //    console.log(this.feError);
    //    }
    //ngOnInit() {
    //    debugger;
    //    console.log(this.feError);
    //    }

    ngDoCheck() {
        //debugger;
        //console.log(this.feError);
    }

    //    ngAfterContentInit() {
    //    }
    //    ngAfterContentChecked() {
    //    }
    //    ngAfterViewInit() {
    //    }

    ngAfterViewChecked() {
        //debugger;
        //console.log(this.feError);

    }

    //    ngOnDestroy() {
    //    }


}