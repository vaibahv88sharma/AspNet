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
import { FormGroupDetails } from "../model/form-elements";
import { ComponentMessageService } from '../../shared/services/component-message.service';

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

    public isUnderstandValue = false;
    //public PaginationButtonEvent ev = new PaginationButtonEvent();

    ngOnChanges() {
    }
    ngOnInit() {
    }
    ngDoCheck() {
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
    }

    public isUnderstandChange(isUnderstandValue: boolean, e: any): void {
        //debugger;
        this.isUnderstandEvent.emit(isUnderstandValue);
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
    }

}
