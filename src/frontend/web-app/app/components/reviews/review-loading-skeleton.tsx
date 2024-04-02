import Skeleton from 'react-loading-skeleton';

export default function ReviewSkeleton() {
    return (
        <div className="bg-secondary flex-grow bg-opacity-10 p-4 rounded-lg shadow-3xl">
            <div className="bg-header bg-opacity-75 p-4 rounded-lg">
                <div className="flex justify-between items-center mb-2">
                    <h2 className="text-2xl font-semibold text-third"><Skeleton height={30} width={150} /></h2>
                    <div><Skeleton circle={true} height={30} width={30} /></div>
                </div>
                <p className="text-gray-700 cursor-pointer">
                    <Skeleton count={1} height={20} />
                </p>
                <p className="text-right text-xs font-semibold text-third"><Skeleton height={20} width={100} /></p>
            </div>
        </div>
    )
}
