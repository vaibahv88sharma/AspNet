import { PaginationValidation } from "./pagination-validation";

export class FormElements {

    constructor(
        public vrt_title: string = '',
        public firstName: string = '',
        public lastname: string = '',
        public mobilephone: string = '',
        public emailaddress1: string = '',
        public txtConfirmEmail: string = '',
        public vrt_course: string = '',
        public txtCampus: string = '',
        public birthdate: string = '',
        public studentGender: string = '',
        public address1_postalcode: string = '',
        public vrt_whatbroughtyoutothekanganinstitutewebsite: string = '',
        public vrt_kibtstudentidnumber: string = '',
        public vrt_australiancitizenshipresidency: string = '',
        public vrt_aboriginalortorresstraitislander: string = '',
        public vrt_successfullycompletedqualifications: string = '',
        public txtQualification: string = '',
        public vrt_uniquestudentidentifier: string = '',
        public streetNumber: string = '',
        public streetName: string = '',
        public city: string = '',
        public state: string = '',
        public vrt_CityorTownofBirth: string = '',
        public vrt_CountryofBirth: string = '',
        public vrt_CountryofResidence: string = '',
        public idProofType: string = '',	
        public idProof: string = '',	
    ) { }

}


export class FormGroupDetails {

    public formGroupName: string;
    public nextBtnClicked: boolean;
    public backBtnClicked: boolean;

    constructor(_formGroupName: string, _nextBtnClicked: boolean, _backBtnClicked: boolean) {
        this.formGroupName = _formGroupName;
        this.nextBtnClicked = _nextBtnClicked;
        this.backBtnClicked = _backBtnClicked;
    }

}

export class FormGroupValid {

    public name: string;
    public valid: boolean;

    constructor(_name: string, _valid: boolean) {
        this.name = _name;
        this.valid = _valid;
    }

}

export interface IFormGroupMetadata {
    groupIndex: number;
    groupName: string;
    grouptitle: string;
    hidden: boolean;
    groupValid: boolean;
    paginationValidation: {
    [paginationValidation: string]: PaginationValidation
    };	
}