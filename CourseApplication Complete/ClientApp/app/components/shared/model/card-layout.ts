export class CardLayout {

    //constructor(
    //    public save = 'false',
    //    public chkbx = 'false',
    //    public back = 'false',
    //    public next = 'false'
    //) { }
    //        //cr = new CardLayout();


    public chkbx: boolean;
    public back: boolean;
    public save: boolean;
    public next: boolean;

    constructor(_chkbx: boolean, _back: boolean, _save: boolean,  _next: boolean) {
        this.chkbx = _chkbx;
        this.back = _back;
        this.save = _save;
        this.next = _next;
    }


}