import { FaChild } from "react-icons/fa";
import { RiParentFill } from "react-icons/ri";
import { FaUserShield } from "react-icons/fa";
import { FaLock } from "react-icons/fa";
import { TbRating18Plus } from "react-icons/tb";
import { MovieRating } from "@/types";

interface Props {
    rating: MovieRating
}

export default function Rating({ rating }: Props) {
    let ratingIcon;


    switch (rating) {
        case 'G':
            ratingIcon = <FaChild />;
            break;
        case 'PG':
            ratingIcon = <RiParentFill />;
            break;
        case 'PG13':
            ratingIcon = <FaUserShield />;
            break;
        case 'R':
            ratingIcon = <FaLock />;
            break;
        case 'NC17':
            ratingIcon = <TbRating18Plus />;
            break;
        default:
            ratingIcon = <TbRating18Plus />;
            break;
    }

    return (
        <div className="flex-row items-center text-secondary font-extrabold text-2xl">
            <div className="flex flex-row items-center mr-2">{ratingIcon} <span className="ml-1">{rating}</span></div>
        </div>
    )
}
