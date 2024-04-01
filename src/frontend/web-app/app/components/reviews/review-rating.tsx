import { Rating } from "@smastrom/react-rating";
import { CustomFlowbiteTheme } from "flowbite-react";
import { read } from "fs";
import { Dispatch, FormEventHandler, SetStateAction } from "react";

const Star = (
    <path d="M62 25.154H39.082L32 3l-7.082 22.154H2l18.541 13.693L13.459 61L32 47.309L50.541 61l-7.082-22.152L62 25.154z" />
);

const customStyles = {
    itemShapes: Star,
    boxBorderWidth: 2,

    activeFillColor: ['#FEE2E2', '#FFEDD5', '#FEF9C3', '#ECFCCB', '#D1FAE5'],
    activeBoxColor: ['#da1600', '#db711a', '#dcb000', '#61bb00', '#009664'],
    activeBoxBorderColor: ['#c41400', '#d05e00', '#cca300', '#498d00', '#00724c'],

    inactiveFillColor: 'white',
    inactiveBoxColor: '#dddddd',
    inactiveBoxBorderColor: '#a8a8a8',
};

interface Props {
    value: number,
    onChange?: any | undefined,
    readonly?: boolean | undefined
}


export default function ReviewRating({ value, onChange, readonly = false }: Props) {
    return (
        <Rating itemStyles={customStyles} spaceBetween='small' value={value} readOnly={readonly} style={{ maxWidth: 160 }} onChange={onChange} />
    )
}