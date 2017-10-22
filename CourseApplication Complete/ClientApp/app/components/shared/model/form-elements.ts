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
        public birthdate: string = ''
    ) { }

}


export class FormGroupDetails {

    public formGroupName: string;
    //public formGroupTitle: string;
    public nextBtnClicked: boolean;
    public backBtnClicked: boolean;

    constructor(_formGroupName: string, _nextBtnClicked: boolean, _backBtnClicked: boolean) {
        this.formGroupName = _formGroupName;
        //this.formGroupTitle = _formGroupTitle;
        this.nextBtnClicked = _nextBtnClicked;
        this.backBtnClicked = _backBtnClicked;
    }

}

export interface IFormGroupMetadata {
    groupIndex: number;
    groupName: string;
    grouptitle: string;
    hidden: boolean;
}