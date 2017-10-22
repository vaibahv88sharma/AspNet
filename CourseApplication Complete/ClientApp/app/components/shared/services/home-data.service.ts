import { Injectable } from '@angular/core';
import { Http, Headers, Response } from "@angular/http";
import { Observable } from "rxjs/Rx";

@Injectable()
export class HomeDataService {

    private authToken: string = 'a192f036-a552-4066-9e60-9a5d34eed1d0';
    private urlEmailValidate: string = 'https://api.experianmarketingservices.com/query/EmailValidate/1.0';
    private email1: string = "";

    constructor(private http: Http) {
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

    private handleSuccess(res: Response) {
        debugger;
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
        //headers.set('Accept', 'application/json;odata=verbose');

        headers.set('Auth-Token', this.authToken);

        switch (verb) {
            case "GET":
                headers.set('content-Type', 'application/application/json');
                break;
            case "POST":
                //headers.set('Content-type', 'application/json;odata=verbose');
                headers.set('content-Type', 'application/application/json');
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
