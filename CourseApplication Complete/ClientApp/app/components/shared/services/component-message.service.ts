import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';
import { FormGroupDetails } from "../model/form-elements";


@Injectable()
export class ComponentMessageService {
    private subject = new Subject<any>();

    sendMessage(message: string) {
        this.subject.next({ text: message });
    }

    clearMessage() {
        this.subject.next();
    }

    getMessage(): Observable<any> {
        return this.subject.asObservable();
    }

    sendbtnClickNotification(message: FormGroupDetails) {
        this.subject.next({ text: message });
    }
    getbtnClickNotification(): Observable<FormGroupDetails> {
        return this.subject.asObservable();
    }
}