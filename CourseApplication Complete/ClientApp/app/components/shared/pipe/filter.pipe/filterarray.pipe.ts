import { Injectable, Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'customfilterarray'
})

@Injectable()
export class CustomFilterArrayPipe implements PipeTransform {
    transform(items: any[], field: string, value: string): any[] {
        //debugger;
        if (!items) return [];
        return items.filter(it => it[field] == value);
    }
}