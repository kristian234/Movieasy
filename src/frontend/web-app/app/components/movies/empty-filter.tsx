import { useParamsStore } from "@/hooks/useParamsStore"
import ResultPage from "../shared/result-page";

interface Props {
    title?: string,
    subtitle?: string,
    showReset?: boolean
}

export default function EmptyFilter({
    title = "We couldn't find anything!",
    subtitle = 'Try changing or resetting the filters',
    showReset = true
}: Props) {
    const reset = useParamsStore(state => state.reset);

    return (
        <div className="flex p-6 flex-col gap-2 justify-center items-center shadow-md">
            <ResultPage title={title} subtitle={subtitle} center />
            <div className="mt-4">
                {showReset && (
                    <button className="
                        bg-transparent
                        hover:bg-header
                        text-secondary
                        border
                        border-third
                        font-bold 
                        py-2
                        px-4
                        rounded-full
                    "
                        onClick={reset}>Remove Filters</button>
                )}
            </div>
        </div >
    )
}