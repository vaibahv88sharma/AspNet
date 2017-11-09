export class StudentApplicationDataLookup {
    course: Array<ICourse>;
    campus: Array<ICampus>;
    courseCampus: Array<ICourseCampus>;
    vrt_australiancitizenshipresidency: Array<IResidency>;
    vrt_aboriginalortorresstraitislander: Array<IAboriginal>;
    txtQualification: Array<IQualification>
    state: Array<IState>
    country: Array<ICountry>
    idProof: Array<IIdProof>
    whatBroughtYouHere: Array<IWhatBroughtYouHere>
}

export interface ICourse {
    courseID: number;
    courseCode: string;
    courseName: string;
}

export interface ICampus {
    campusID: number;
    campusName: string;
}

export interface ICourseCampus {
    campusID: number;
    campusName: string;
    courseID: number;
    courseCampusID: number;
}

export interface IResidency {
    residencyId: number;
    vrt_australiancitizenshipresidency: string;
    type: number;
}

export interface IAboriginal {
    statusId: number;
    vrt_aboriginalortorresstraitislander: string;
}

export interface IQualification {
    qualification: string;
    selected: boolean;
    internalName: string;
    qualificationID: string;
    qualificationCode: string;
}

export interface IState {
    stateID: number;
    state: string;
}

export interface ICountry {
    stateID: number;
    state: string;
}

export interface IIdProof {
    idProofText: string;
    idProofName: string;
    proofId: number;
    type: string;
    internalName: string;
    courseCampusID: number;
}

export interface IWhatBroughtYouHere {
    vrt_whatbroughtyoutothekanganinstitutewebsite: string;
    reasonToChooseBKIID: number;
}
