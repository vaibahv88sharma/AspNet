export class FormElements {

    //public vrt_title: string;
    //public firstName: string;
    //public lastname: string;

    //constructor(_vrt_title: string, _firstName: string, _lastname: string) {
    //    this.vrt_title = _vrt_title;
    //    this.firstName = _firstName;
    //    this.lastname = _lastname;
    //}

    constructor(
        public vrt_title: string = '',
        public firstName: string = '',
        public lastname: string = '',
        public emailaddress1: string = '',
        public txtConfirmEmail: string = '',
    ) {}

}