export interface IProgrammeListElement {
    id: number;
    title: string;
    iconUrl: string;
    emissions: {
        channel: {
            id: number,
            name: string
        },
        start: string,
        stop: string
    } [];
    features: {
        id: number,
        type: string,
        value: string
    } [];
    description: string;
    rating: number;
}
