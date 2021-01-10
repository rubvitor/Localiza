import { DivisorPrimoModel } from "./divisor-primoModel";

export class ValidationResult {
    isValid: boolean;
    errors: ValidationFailureModel[];
    ruleSetsExecuted: string[];
}

export class ValidationResultModel {
    isValid: boolean;
    errors: ValidationFailureModel[];
    ruleSetsExecuted: string[];
    objectResult?: DivisorPrimoModel;
}

export class ValidationFailureModel {
    propertyName: string;
    errorMessage: string;
    attemptedValue: any;
    customState: any;
    severity: Severity;
    errorCode: string;
    formattedMessageArguments: any[];
    formattedMessagePlaceholderValues: any;
}

export enum Severity {
    error = 0,
    warning = 1,
    info = 2
}
