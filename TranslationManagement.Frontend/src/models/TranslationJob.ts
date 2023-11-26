


export interface TranslationJob {
    id: number;
    customerName: string;
    status: "New" | "InProgress" | "Completed";
    originalContent: string;
    translatedContent: string;
    translatorId?: number;
    price: number;
}

export interface PostTranslationJob {
    customerName: string;
    originalContent?: string;
    fileToTranslate?: File;
}