import { Review } from "@/types"
import ReviewRating from "./review-rating"

interface Props {
    userName: string,
}

export default function ReviewCard({ }: Props) {
    return (
        <div className="bg-secondary bg-opacity-10 p-4 rounded-lg shadow-3xl">
            <div className="bg-header bg-opacity-75 p-4 rounded-lg">
                <div className="flex justify-between items-center mb-2">
                    <h2 className="text-2xl font-semibold text-third">Kristian Teodosiev</h2>
                    <ReviewRating value={3} readonly={true} />
                </div>
                <p className="text-gray-700 cursor-pointer">
                    Cras at turpis lacus. Mauris vehicula nisi eget placerat mollis. Aenean hendrerit risus urna, vel congue nunc tristique id. Pellentesque a sagittis urna. Duis sollicitudin consectetur egestas. Pellentesque feugiat eros a tellus convallis, eu volutpat est viverra. Donec eget faucibus diam. Curabitur feugiat imperdiet ipsum. In et ante a mi suscipit condimentum. Mauris egestas finibus justo in tincidunt. Fusce imperdiet consequat vulputate.
                </p>
                <p className="text-right text-xs font-semibold text-third">Posted on 2020-15-4</p>
            </div>
        </div>
    )
}