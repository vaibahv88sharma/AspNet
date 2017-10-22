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

@Component({
    selector: 'pagination-control',
    templateUrl: './pagination-control.component.html',
    styleUrls: ['./pagination-control.component.css']
})
export class PaginationControlComponent
    implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy
{
    //@Input('cardLyt')
    //public cardLayout: CardLayout;

    @Input('pv')
    public paginationValidation: PaginationValidation;

    @Output() isUnderstandEvent = new EventEmitter<boolean>();
    @Output() buttonEvent = new EventEmitter<PaginationButtonEvent>();

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
    public nextClick(e: any, parentName: string): void {
        //debugger;
        let ev = new PaginationButtonEvent('next', true, parentName);
        this.buttonEvent.emit(ev);
    }

}
