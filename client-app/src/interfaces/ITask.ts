export interface ITask {
    id?: string | null;
    name: string;
    priorityValue?: number | null;
    status?: number | null;
    statusText?: string | null;
    nameError?: string | null;
}