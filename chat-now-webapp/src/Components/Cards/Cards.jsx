export const Card = ({ userImg, userName, lastMessage }) => {
    return (
        <div className="flex flex-col gap-4 w-[48%] h-36 p-[10px] bg-[#D9D9D9] rounded-2xl">
            <div className="flex flex-row items-center justify-between gap-2">
                <img className="w-11 h-11 bg-purple rounded-2xl" src={userImg} alt="" />
                
                <h2 className="w-[78%] font-NotoThai font-bold text-sm">{userName}</h2>
            </div>

            <p className="font-NotoThai text-xs">{lastMessage}</p>
        </div>
    );
}