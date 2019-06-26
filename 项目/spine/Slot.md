### SlotData

SlotData是存储的初始Slot信息，Slot是依附于骨骼的，spine的绘制顺序是机遇slot生成的，其中，图片、网格或者其他附件都是附加在slot上，在插槽列表中位于最上面的附件最后绘制。每一个slot都关联到一个骨骼，和多个附件（可能为空，但是看代码，是**只有一个attachment位置的**）

