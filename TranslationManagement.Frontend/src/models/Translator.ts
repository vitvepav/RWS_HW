


export interface Translator {
    id: number;
    name: string;
    hourlyRate: number;
    status: "Applicant" | "Certified" | "Deleted";
    creditCardNumber: string;
}