export const ContactMessage = ({ message, sendTime }) => {
    return (
        <div className="flex flex-col self-start w-[80%] rounded-lg pt-4 px-6 bg-light-purple">
            <p className="90% font-NotoThai text-base">{message}</p>
            <p className="10% pt-2 text-end font-NotoKannada font-bold">{sendTime}</p>
        </div>
    );
}

export const MyMessage = ({ message, sendTime }) => {
    return (
        <div className="flex flex-col self-end w-[80%] rounded-lg pt-4 px-6 bg-purple">
            <p className="90% font-NotoThai text-base">{message}</p>
            <p className="10% pt-2 text-end font-NotoKannada font-bold">{sendTime}</p>
        </div>
    );
}