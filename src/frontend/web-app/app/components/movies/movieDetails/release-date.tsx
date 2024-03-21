import { FaRegCalendarAlt } from "react-icons/fa";

interface Props {
    date: Date | null
}

export default function ReleaseDate({ date }: Props) {
    return (
        <div className="flex flex-wrap">
            <div className="flex flex-row text-secondary text-1xl items-center mr-2">
                <FaRegCalendarAlt />
                <span className="ml-1">{date != null && date.toLocaleDateString()}</span>
            </div>
        </div>
    )
}