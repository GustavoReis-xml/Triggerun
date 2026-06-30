const ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_`abcdefghijklmnopqrstuvwxyz";
const s_chr64toint = {};
for (let i = 0; i < 64; i++) {
    let c = ALPHABET[i];
    s_chr64toint[c] = i;
}

function GetFromInt64(str) {
    if (!str) return 0;
    let num = s_chr64toint[str[0]];
    if (num === undefined) {
        throw new Error("Invalid char: " + str[0] + " in token " + str);
    }
    for (let i = 1; i < str.length; i++) {
        num *= 64;
        let val = s_chr64toint[str[i]];
        if (val === undefined) {
            throw new Error("Invalid char: " + str[i] + " in token " + str);
        }
        num += val;
    }
    return num;
}

function CreateWithShortStr(sAgv) {
    if (sAgv === "") return null;
    let stack = [];
    let arrayList = [];
    let array = sAgv.split("");
    let i = 0;
    let num = 0;
    let num2 = array.length;
    let arrayList2 = null;
    let text = "";
    let num3 = 1;
    let flag = false;
    
    if (array[i] === '(' && array[num2 - 1] === ')') {
        stack.push(arrayList);
        arrayList2 = arrayList;
        i = 1;
        num2--;
    } else {
        stack.push(arrayList);
        arrayList2 = arrayList;
    }
    
    while (i < num2) {
        let c = array[i];
        text = "";
        switch (c) {
            case '"': {
                let num4 = 0;
                let j;
                for (j = i + 1; j < num2; j++) {
                    num4++;
                    if (array[j] === '"') {
                        text = array.slice(i + 1, j).join("");
                        arrayList2.push(text);
                        i = j;
                        num3 = 1;
                        num = i + 1;
                        break;
                    }
                }
                if (j === num2) return null;
                break;
            }
            case '\'': {
                let num4 = 0;
                let j;
                for (j = i + 1; j < num2; j++) {
                    num4++;
                    if (array[j] === '\'') {
                        text = array.slice(i + 1, j).join("");
                        arrayList2.push(text);
                        i = j;
                        num3 = 1;
                        num = i + 1;
                        break;
                    }
                }
                if (j === num2) return null;
                break;
            }
            case '(': {
                let arrayList3 = [];
                arrayList2.push(arrayList3);
                arrayList2 = arrayList3;
                stack.push(arrayList3);
                num3 = 1;
                num = i + 1;
                break;
            }
            case ')': {
                stack.pop();
                arrayList2 = stack[stack.length - 1];
                if (!arrayList2) {
                    console.log("Unbalanced pop at index", i);
                    return null;
                }
                num3 = 1;
                num = i + 1;
                break;
            }
            case ',': {
                if (array[i + 1] !== '\'' && array[i + 1] !== '"' && array[i + 1] !== '(') {
                    if (array[i + 1] === '-') {
                        num3 = -1;
                    } else {
                        num3 = 1;
                    }
                    let j;
                    for (j = i + 1; j < num2 + 1; j++) {
                        text += array[j] || "";
                        if (array[j] === ',' || array[j] === ')' || array[j] === '(') {
                            text = text.substring(0, text.length - 1);
                            let num5 = num3 * GetFromInt64(text);
                            arrayList2.push(num5);
                            i = j - 1;
                            break;
                        }
                    }
                    if (j === num2 + 1) return null;
                }
                break;
            }
            default: {
                if (c !== '\\') {
                    num3 = 1;
                    flag = false;
                    let j;
                    for (j = i; j < num2; j++) {
                        text += array[j];
                        if (array[j] === ',' || array[j] === ')' || array[j] === '(') {
                            text = text.substring(0, text.length - 1);
                            let num5 = num3 * GetFromInt64(text);
                            arrayList2.push(num5);
                            i = j - 1;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag) {
                        let num5 = num3 * GetFromInt64(text);
                        arrayList2.push(num5);
                        i = j;
                    }
                    if (j === num2 + 1) return null;
                } else if (array[i + 1] === '"' || array[i + 1] === '\'') {
                    i++;
                }
                break;
            }
        }
        i++;
    }
    return arrayList;
}

const packet = `("PlayerSelect",(((2,'4a6f6761646f725f31',1,Ff,0,0,1,1,1,0,0,(Ff,(1,0,1,'2026/05/31 01:39:43',0,(),(1,(1,1,0,0,'2026/05/31 01:39:43',0),2,(2,1,0,0,'2026/05/31 01:39:43',0),3,(3,1,0,0,'2026/05/31 01:39:43',0),4,(4,1,0,0,'2026/05/31 01:39:43',0),5,(5,1,0,0,'2026/05/31 01:39:43',0),6,(6,1,0,0,'2026/05/31 01:39:43',0),7,(7,1,0,0,'2026/05/31 01:39:43',0),8,(8,1,0,0,'2026/05/31 01:39:43',0),9,(9,1,0,0,'2026/05/31 01:39:43',0),A,(A,1,0,0,'2026/05/31 01:39:43',0),B,(B,1,0,0,'2026/05/31 01:39:43',0),C,(C,1,0,0,'2026/05/31 01:39:43',0),D,(D,1,0,0,'2026/05/31 01:39:43',0),E,(E,1,0,0,'2026/05/31 01:39:43',0),F,(F,1,0,0,'2026/05/31 01:39:43',0)),(1,(1,1,0,0,'2026/05/31 01:39:43',0,()),2,(2,1,0,0,'2026/05/31 01:39:43',0,()),3,(3,1,0,0,'2026/05/31 01:39:43',0,()),4,(4,1,0,0,'2026/05/31 01:39:43',0,()),5,(5,1,0,0,'2026/05/31 01:39:43',0,()),6,(6,1,0,0,'2026/05/31 01:39:43',0,()),7,(7,1,0,0,'2026/05/31 01:39:43',0,()),8,(8,1,0,0,'2026/05/31 01:39:43',0,()),9,(9,1,0,0,'2026/05/31 01:39:43',0,()),A,(A,1,0,0,'2026/05/31 01:39:43',0,()),B,(B,1,0,0,'2026/05/31 01:39:43',0,()),C,(C,1,0,0,'2026/05/31 01:39:43',0,()),D,(D,1,0,0,'2026/05/31 01:39:43',0,()),E,(E,1,0,0,'2026/05/31 01:39:43',0,()),F,(F,1,0,0,'2026/05/31 01:39:43',0,())),(1,(1,1,0,0,'2026/05/31 01:39:43',0),2,(2,1,0,0,'2026/05/31 01:39:43',0),3,(3,1,0,0,'2026/05/31 01:39:43',0),4,(4,1,0,0,'2026/05/31 01:39:43',0),5,(5,1,0,0,'2026/05/31 01:39:43',0),6,(6,1,0,0,'2026/05/31 01:39:43',0),7,(7,1,0,0,'2026/05/31 01:39:43',0),8,(8,1,0,0,'2026/05/31 01:39:43',0),9,(9,1,0,0,'2026/05/31 01:39:43',0),A,(A,1,0,0,'2026/05/31 01:39:43',0),B,(B,1,0,0,'2026/05/31 01:39:43',0),C,(C,1,0,0,'2026/05/31 01:39:43',0),D,(D,1,0,0,'2026/05/31 01:39:43',0),E,(E,1,0,0,'2026/05/31 01:39:43',0),F,(F,1,0,0,'2026/05/31 01:39:43',0)),(1,(1,1,0,0,'2026/05/31 01:39:43',0),2,(2,1,0,0,'2026/05/31 01:39:43',0),3,(3,1,0,0,'2026/05/31 01:39:43',0),4,(4,1,0,0,'2026/05/31 01:39:43',0),5,(5,1,0,0,'2026/05/31 01:39:43',0),6,(6,1,0,0,'2026/05/31 01:39:43',0),7,(7,1,0,0,'2026/05/31 01:39:43',0),8,(8,1,0,0,'2026/05/31 01:39:43',0),9,(9,1,0,0,'2026/05/31 01:39:43',0),A,(A,1,0,0,'2026/05/31 01:39:43',0),B,(B,1,0,0,'2026/05/31 01:39:43',0),C,(C,1,0,0,'2026/05/31 01:39:43',0),D,(D,1,0,0,'2026/05/31 01:39:43',0),E,(E,1,0,0,'2026/05/31 01:39:43',0),F,(F,1,0,0,'2026/05/31 01:39:43',0)),0,1,1,1,1,0)),0,(),1,1),(1,Fc,Fc,1,1,1,1,1,1,2SG,1,1,1,1,1,1,1,1,1,1),(1HK,2PA,17u,vg,0,0,0,2o),(),(),('Local',1,1,1,1,0,'',1),(),()),(1,0,((0),(),(A)),Ff,(0,((3,2SG))),((Fl,0,1,0,1,0,0,0,0,0,0),(Fj,0,1,0,1,0,0,0,0,0,0),(Fp,0,1,0,1,0,0,0,0,0,0)),(),())))`;

try {
    const res = CreateWithShortStr(packet);
    if (res) {
        console.log("SUCCESSFULLY PARSED!");
    } else {
        console.log("FAILED TO PARSE (Returned null)");
    }
} catch (e) {
    console.error("ERROR DURING PARSE:", e);
}
