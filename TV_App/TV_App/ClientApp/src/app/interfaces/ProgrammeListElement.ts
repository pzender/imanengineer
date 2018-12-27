export interface IProgrammeListElement {
    title : string,
    iconUrl : string,
    emissions : {
        channelName : string,
        start : string,
        stop : string
    } [],
    features : {
        type : string,
        value : string
    } []

}