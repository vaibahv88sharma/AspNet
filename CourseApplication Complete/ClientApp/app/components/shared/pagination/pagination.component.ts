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
    ViewChild,
    ContentChild,
} from '@angular/core';
import { CardLayout } from '../model/card-layout';

@Component({
    selector: 'app-pagination',
    templateUrl: './pagination.component.html',
    styleUrls: ['./pagination.component.css']
})
export class PaginationComponent
    implements OnChanges, OnInit, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy
{
    @Input('cardLyt')
    public cardLayout: CardLayout;

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

}
