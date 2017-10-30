
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'customfilter'
})
export class FilterPipe implements PipeTransform {
    //transform(value: any, args: any[]): any {
    //}
    transform(originalArray: any, inputValue?: any): any {
        //debugger;
        if (inputValue === undefined) {
            return originalArray;
        }
        return originalArray.filter(function (eachRow: any) {
            //debugger;
            return (eachRow.groupName === inputValue);
        })
    }
}
