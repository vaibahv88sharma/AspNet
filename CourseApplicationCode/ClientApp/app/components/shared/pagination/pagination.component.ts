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
export class PaginationComponent{

    @Input('pv')
    public paginationValidation: PaginationValidation;

    @Input('frmGrpNm')
    public formGroupName: string;

    public isUnderstandValue = false;
    fromCAPaginationMessageSubscription: Subscription;
    fromCAFormGroupValidMessage: FormGroupValid;

    public isUnderstandChange(isUnderstandValue: boolean, e: any): void {
        if (isUnderstandValue) {
            (<any>this.paginationValidation).nextBtnDisable = false;
        } else {
            (<any>this.paginationValidation).nextBtnDisable = true;
        }
    }

    constructor(private cms: ComponentMessageService) {
    }

    public btnEvent(e: any, parentName: string, btnName: string): void {
        let ev = new FormGroupDetails(
            parentName, //"",
            btnName == 'next' ? true : false,
            btnName == 'back' ? true : false
        );
        this.cms.sendbtnClickNotification(ev);
    }

}
