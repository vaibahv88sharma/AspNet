
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'customfilter'
})
export class FilterPipe implements PipeTransform {
    transform(originalArray: any, inputValue?: any): any {
        if (inputValue === undefined) {
            return originalArray;
        }
        return originalArray.filter(function (eachRow: any) {
            return (eachRow.groupName === inputValue);
        })
    }
}
