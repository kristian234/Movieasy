interface Props {
    title: string,
    subtitle: string,
    center?: boolean
}

export default function ResultPage({ title, subtitle, center }: Props) {
    return (
        <div className={center ? 'text-center' : 'text-start'}>
            <div className="text-2xl font-bold text-secondary">
                {title}
            </div>
            <div className="font-light text-secondary mt-2">
                {subtitle}
            </div>
        </div>
    )
}