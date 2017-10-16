import {
    Component, OnInit, Input, OnChanges, DoCheck, AfterViewChecked, } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';  //Reactive Forms
import { FormElements } from "../../../shared/model/form-elements";

@Component({
  selector: 'email-controls',
  templateUrl: './email-controls.component.html',
  styleUrls: ['./email-controls.component.css']
})
export class EmailControlsComponent implements OnInit, OnChanges, DoCheck, AfterViewChecked
{

  @Input('emailGrp')
  public emailGroupForm: FormGroup;

  @Input('egErr')
  public egError: FormElements;

  constructor() { }

  ngOnChanges() {
    //debugger;
  }  

  ngOnInit() {
      //debugger;
      console.log(this.emailGroupForm);
      console.log(this.egError);      
  }

  ngDoCheck() {
      //debugger;
      //console.log(this.feError);
  }

  ngAfterViewChecked() {
      //debugger;
      if (this.emailGroupForm) {
          //console.log("confirm : " + this.emailGroupForm.errors);
      }
      //console.log("confirm : "+ this.emailGroupForm.errors!.match);
      //console.log(this.feError);

  }

}
