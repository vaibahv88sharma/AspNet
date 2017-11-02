/* Defines the Pagination entity */
export class PaginationValidation {
    constructor(
        public yesNoHide: boolean = false,
        public yesNoDisable: boolean = false,
        public backBtnHide: boolean = false,
        public backBtnDisable: boolean = false,
        public saveBtnHide: boolean = false,
        public saveBtnDisable: boolean = false,
        //public nextBtnHideHide: boolean = false,
        public nextBtnHide: boolean = false,
        //public nextBtnHideDisable: boolean = false,
        public nextBtnDisable: boolean = false,
        public parentName: string = "",
    ) { }
}


export class PaginationButtonEvent {
    constructor(
        public buttonName: string = '',
        public buttonClicked: boolean = false,
        public parent: string = '',
    ) { }
}