import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dataType'
})
export class DataTypePipe implements PipeTransform {
  objValues: any [] = [];

  transform(tableObj: any, key: any, ...args: unknown[]): unknown {
    if (Array.isArray(tableObj[key])) {
      for (let item of tableObj[key]) {
        console.log('item', item);
      }
    }
    // this.objValues = Object.values(tableObj);
    // for (let item of this.objValues)
    // {
    //   if (Array.isArray(item)) {
    //     console.log('HIT', item);
    //   }
      
    // }

    return null;
  }

}
