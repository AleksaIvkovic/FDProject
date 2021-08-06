import { FileData } from "./file.model";

export class FileGroup {
    constructor(
        public type: string,
        public files: FileData[]
    ){}
}