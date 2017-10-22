import { FormGroup, AbstractControl, FormArray, FormControl } from '@angular/forms';  //Reactive Forms

export class CommonMethods {

    //public static formHasError(c: FormGroup): boolean {
    //    debugger;
    //    let returnValue = false;
    //    Object.keys(c.controls).forEach((name) => {
    //        debugger;
    //        let formControl = c.controls[name];
    //        if (formControl instanceof FormGroup) {
    //            debugger;
    //            if (formControl.errors) {
    //                debugger;
    //                //return true;
    //                returnValue = true;
    //                return returnValue;
    //                //break;
    //            }
    //            this.formHasError(formControl);
    //        } else {
    //            //debugger;
    //            if (formControl instanceof FormArray) {
    //                debugger;
    //                this.formHasError(<FormGroup>formControl.controls[0]);
    //            } else {
    //                debugger;
    //                if (formControl.errors) {
    //                    debugger;
    //                    //return true;
    //                    returnValue = true;
    //                    return returnValue;
    //                    //break;
    //                }
    //            }
    //        }
    //        debugger;

    //    });
    //    debugger;
    //    //return false;
    //    return returnValue;
    //}

    public static formHasError(c: FormGroup, initialReturnValue: boolean): boolean {
        //debugger;
        //let returnValue = false;
        Object.keys(c.controls).forEach((name) => {
            //debugger;
            let formControl = c.controls[name];
            if (formControl instanceof FormGroup) {
                //debugger;
                if (formControl.errors) {
                    //debugger;
                    //return true;
                    initialReturnValue = true;
                    return initialReturnValue;
                    //break;
                }
                this.formHasError(formControl, initialReturnValue);
            } else if (formControl instanceof FormArray) {
                //debugger;
                this.formHasError(<FormGroup>formControl.controls[0], initialReturnValue);
            } else if (formControl instanceof FormControl) {
                //debugger;
                if (formControl.errors) {
                    //debugger;
                    //return true;
                    initialReturnValue = true;
                    return initialReturnValue;
                    //break;
                }
            } else {
                //debugger;
            }
            //debugger;
        });
        //debugger;
        //return false;
        return initialReturnValue;
    }

}