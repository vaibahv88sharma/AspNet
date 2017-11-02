//import { Component, Input } from '@angular/core';
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
    Output,
    EventEmitter,
    ViewChild,
    ContentChild,
} from '@angular/core';
import { CardLayout } from '../model/card-layout';
import { PaginationValidation, PaginationButtonEvent } from "../model/pagination-validation";
import { FormGroupDetails, FormGroupValid } from "../model/form-elements";
import { ComponentMessageService } from '../../shared/services/component-message.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'app-pagination',
    templateUrl: './pagination.component.html',
    styleUrls: ['./pagination.component.css']
})
export class PaginationComponent
    implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy {
    //@Input('cardLyt')
    //public cardLayout: CardLayout;

    @Input('pv')
    public paginationValidation: PaginationValidation;

    @Output() isUnderstandEvent = new EventEmitter<boolean>();
    //@Output() buttonEvent = new EventEmitter<PaginationButtonEvent>();
    @Output() buttonEvent = new EventEmitter<FormGroupDetails>();

    @Input('frmGrpNm')
    public formGroupName: string;

    public isUnderstandValue = false;
    //private paginationValidation: PaginationValidation = new PaginationValidation();
    //public PaginationButtonEvent ev = new PaginationButtonEvent();
    fromCAPaginationMessageSubscription: Subscription;
    fromCAFormGroupValidMessage: FormGroupValid;

    ngOnChanges() {
    }
    ngOnInit() {
        //if (this.formGroupName == 'piGroup') {
        //    this.paginationValidation = new PaginationValidation(false, true, true, true, true, true, false, true, this.formGroupName);
        //} else {
        //    this.paginationValidation = new PaginationValidation(true, true, false, false, true, true, false, false, this.formGroupName);
        //}

        //this.fromCAPaginationMessageSubscription = this.cms.getFormgroupValidNotification().subscribe(message => {
        //    //debugger;
        //    this.fromCAFormGroupValidMessage = (<any>message).text;
        //    //debugger;
        //    if (this.fromCAFormGroupValidMessage.valid) {
        //        if (this.fromCAFormGroupValidMessage.name == this.formGroupName) {
        //            //debugger;
        //            this.paginationValidation = new PaginationValidation(false, false, true, true, true, true, false, true, this.formGroupName);
        //            if (this.isUnderstandValue) {
        //                //debugger;
        //                this.paginationValidation = new PaginationValidation(false, false, true, true, true, true, false, false, this.formGroupName);
        //            } else {
        //                this.paginationValidation = new PaginationValidation(false, false, true, true, true, true, false, true, this.formGroupName);
        //            }
        //        }
        //        else if (this.fromCAFormGroupValidMessage.name == this.formGroupName) {
        //            //debugger;
        //            this.paginationValidation = new PaginationValidation(true, true, false, false, true, true, false, false, this.formGroupName);
        //        }
        //    } else {
        //        if (this.fromCAFormGroupValidMessage.name == this.formGroupName) {
        //            //debugger;
        //            this.paginationValidation = new PaginationValidation(false, true, true, true, true, true, false, true, this.formGroupName);
        //        }
        //        else if (this.fromCAFormGroupValidMessage.name == this.formGroupName) {
        //            //debugger;
        //            this.paginationValidation = new PaginationValidation(false, true, true, true, true, true, false, true, this.formGroupName);
        //        }
        //    }
        //});
    }
    ngDoCheck() {
        //if (this.isUnderstandValue) {
        //    debugger;
        //    console.log('isUnderstandValue:-   ' + this.isUnderstandValue);
        //}
    }
    ngAfterContentInit() {
    }
    ngAfterContentChecked() {
    }
    ngAfterViewInit() {
    }
    ngAfterViewChecked() {
    }
    ngOnDestroy() {
     //   this.fromCAPaginationMessageSubscription.unsubscribe();
    }

    public isUnderstandChange(isUnderstandValue: boolean, e: any): void {
        //debugger;
        //this.isUnderstandEvent.emit(isUnderstandValue);
        if (isUnderstandValue) {
            //debugger;
            //this.paginationValidation = new PaginationValidation(false, false, true, true, true, true, false, false, 'piGroup');
            (<any>this.paginationValidation).nextBtnDisable = false;
        } else {
            //this.paginationValidation = new PaginationValidation(false, false, true, true, true, true, false, true, 'piGroup');
            (<any>this.paginationValidation).nextBtnDisable = true;
        }
        //console.log('checked');
    }

    constructor(private cms: ComponentMessageService) {
        //cr = new CompanyRegister();
    }

    //sendMessage(): void {
    //    // send message to subscribers via observable subject
    //    debugger;
    //    this.cms.sendMessage('Message from Home Component to App Component!');
    //}

    //public nextClick(e: any, parentName: string): void {
    //    //debugger;
    //    let ev = new PaginationButtonEvent('next', true, parentName);
    //    this.buttonEvent.emit(ev);
    //}

    //this.fromCAPaginationMessageSubscription = this.cms.getbtnClickNotification().subscribe(message => {
    //    //debugger;
    //    this.paginationMessage = (<any>message).text;
    //    //debugger;
    //    //this.cms.clearSubjectMessage();
    //});

    public btnEvent(e: any, parentName: string, btnName: string): void { //parentTitle: string, 
        //debugger;
        let ev = new FormGroupDetails(
            parentName, //"",
            btnName == 'next' ? true : false,
            btnName == 'back' ? true : false
        );
        this.buttonEvent.emit(ev);
        //debugger;
        //this.cms.sendMessage('Message from Home Component to App Component!');
        this.cms.sendbtnClickNotification(ev);
        //
    }

}
