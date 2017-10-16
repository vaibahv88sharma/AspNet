import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-card-projection',
    templateUrl: './card.component.html',
    styleUrls: ['./card.component.css']
})
export class CardComponent {
    @Input('tabTitle') title: string;
}
