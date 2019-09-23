x = [1.5,7.5,13.5,28.5,41.5,55.5,71.5,239.5]
Period=0.125#us
ChannelNum=3#通道个数
def NeedTimeAllChannelOneData(x):
    Res = (x + 12.5)*Period*ChannelNum
    return Res
def ArrayDeep20ms(x):
    Res = 20000/NeedTimeAllChannelOneData(x)
    return Res
for i in x:
    print ("SampleTime:%10.5f ===> Array Deep:%10.5f "%(i,ArrayDeep20ms(i)))
