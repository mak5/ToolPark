export class Tool{
    constructor(
        public serialNumber: number,
        public currentSerialNumber: number,
        public label: string,
        public imageUrl: string,
        public nextInspectionDate: Date
    ){}
}