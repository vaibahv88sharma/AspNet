import { Injectable } from '@angular/core';
import { Http, Headers, Response } from "@angular/http";
import { Observable } from "rxjs/Rx";
//import { AppConfigurableSettings } from './app-configurable.settings';

@Injectable()
export class HomeDataService {

    private authToken: string = 'a192f036-a552-4066-9e60-9a5d34eed1d0';
    private urlEmailValidate: string = 'https://api.experianmarketingservices.com/query/EmailValidate/1.0';
    private email1: string = "";

    constructor(private http: Http,
        //private aps: AppConfigurableSettings
    ) {
    }

    postEmailValidate(email: string) {
        //postEmailValidate(email: string): Observable < any > {
        let data = {
            "Email": email,
            "Timeout": "5",
            "Verbose": "True"
        };
        debugger;
        return this.http.post(
            this.urlEmailValidate,
            data,
            { headers: this.getHeaders("POST") }
        )
            //.delay(5000)
            .map(this.handleSuccess)
            .catch(this.handleError)
    }

    getData(url: string): Observable<any> {
        return this.http.get(
            url
        )
            //.delay(5000)
            .map(this.handleSuccess)
            .catch(this.handleError)
    }

    postgetGetApplicationLookups(url: string) {
        let data = {};
        debugger;
        return this.http.post(
            url,//AppConfigurableSettings.DATA_API,
            data,
            { headers: this.getHeaders("POST.NET") /* , withCredentials: true*/ }
        )
            //.delay(5000)
            .map(this.handleSuccess)
            .catch(this.handleError)
    }

    getApplicationLookups(url: string) {
        let data = {};
        //debugger;
        return this.http.get(
            //'http://webapp01d-doc/CourseEnrolment/Pages/ApplicationData.aspx/GetApplicationAllLookups',
            url,//AppConfigurableSettings.DATA_API,
            //data,
            { headers: this.getHeaders("GET") /* , withCredentials: true*/ }
        )
            //.delay(5000)
            .map(this.handleSuccess)
            .catch(this.handleError)
    }

    private handleSuccess(res: Response) {
        //debugger;
        let body = res.json();
        //console.log(body);

        //let products: IProduct[] =[];
        //for(let i in body.d.results){
        //    products.push(
        //        {
        //            'id': body.d.results[i]["Id"],
        //            'productName': body.d.results[i]["productName"],
        //            'productCode': body.d.results[i]["productCode"],
        //            'releaseDate': body.d.results[i]["releaseDate"],
        //            'description': body.d.results[i]["description"],
        //            'price': body.d.results[i]["price"],
        //            'starRating': body.d.results[i]["starRating"],
        //            'imageUrl': body.d.results[i]["imageUrl"],
        //            'tags': ['']
        //        }              
        //    );
        //}
        //return products || {};

        return body || {};
    }

    private handleError(error: any) {
        debugger;
        console.log(error);
        return Observable.throw(error.json());
    }

    //this function resolves the headers according to the verb that we are using
    private getHeaders(verb?: string) {
        var headers = new Headers();
        //var digest = document.getElementById('__REQUESTDIGEST').value;
        //var digest = (<HTMLInputElement>document.getElementById('__REQUESTDIGEST')).value;

        //headers.set('X-RequestDigest', digest);
        headers.set('Accept', 'application/json; charset=utf-8');

        //headers.set('Auth-Token', this.authToken);

        switch (verb) {
            case "GET":
                //headers.set('content-Type', 'application/application/json');
                headers.set('content-Type', 'application/json; charset=utf-8');
                break;
            case "POST.NET":
                //headers.set('Content-type', 'application/json;odata=verbose');
                headers.set('content-Type', 'application/json; charset=utf-8');
                //headers.set('Access-Control-Allow-Origin', '*/*');//'*');
                //headers.set('Access-Control-Allow-Methods','GET, POST, PATCH, PUT, DELETE, OPTIONS');
                //headers.set('Access-Control-Allow-Headers', 'true');
                //headers.set('Access-Control-Allow-Credentials', 'Origin, Content-Type, X-Auth-Token');
                break;
            case "PUT":
                headers.set('Content-type', 'application/json;odata=verbose');
                headers.set("IF-MATCH", "*");
                headers.set("X-HTTP-Method", "MERGE");
                break;
            case "DELETE":
                headers.set("IF-MATCH", "*");
                headers.set("X-HTTP-Method", "DELETE");
                break;
        }
        return headers;
    }

}
