import { Component, Input } from '@angular/core';
import { PaginationButtonEvent } from '../model/pagination-validation';

@Component({
    selector: 'app-card-projection',
    templateUrl: './card.component.html',
    styleUrls: ['./card.component.css']
})
export class CardComponent {
    @Input('tabTitle') title: string;
    @Input('hide') hidden: boolean;// = false;

    //PaginationButtonEvent
    private buttonClick: PaginationButtonEvent = new PaginationButtonEvent();

    buttonClickEvent(nextButtonClick: PaginationButtonEvent): void {
        debugger;
        this.buttonClick = nextButtonClick;
    }

}
