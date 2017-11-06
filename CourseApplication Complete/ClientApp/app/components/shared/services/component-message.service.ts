import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';
import { FormGroupDetails, FormGroupValid } from "../model/form-elements";
//import { PaginationValidation } from "../model/pagination-validation";


@Injectable()
export class ComponentMessageService {
    private subject = new Subject<any>();
    private vrt_kibtstudentidnumberSubject = new Subject<any>();
    private txtQualificationNotificationSubject = new Subject<any>();
    private hasUSINotificationSubject = new Subject<any>();
    private formgroupValidSubject = new Subject<any>();

    //sendMessage(message: string) {
    //    this.subject.next({ text: message });
    //}

    clearSubjectMessage() {
        this.subject.next();
    }

    //getMessage(): Observable<any> {
    //    return this.subject.asObservable();
    //}

    sendbtnClickNotification(message: FormGroupDetails) {
        this.subject.next({ text: message });
    }
    getbtnClickNotification(): Observable<FormGroupDetails> {
        return this.subject.asObservable();
    }

    sendFormgroupValidNotification(message: FormGroupValid) {
        this.formgroupValidSubject.next({ text: message });
    }
    getFormgroupValidNotification(): Observable<FormGroupValid> {
        return this.formgroupValidSubject.asObservable();
    }

    sendVrt_kibtstudentidnumberNotification(message: number) {
        this.vrt_kibtstudentidnumberSubject.next({ text: message });
    }
    getVrt_kibtstudentidnumberNotification(): Observable<number> {
        return this.vrt_kibtstudentidnumberSubject.asObservable();
    }

    sendTxtQualificationNotification(message: number) {
        this.txtQualificationNotificationSubject.next({ text: message });
    }
    getTxtQualificationNotification(): Observable<number> {
        return this.txtQualificationNotificationSubject.asObservable();
    }

    sendHasUSINotification(message: number) {
        this.hasUSINotificationSubject.next({ text: message });
    }
    getHasUSINotification(): Observable<number> {
        return this.hasUSINotificationSubject.asObservable();
    }

}