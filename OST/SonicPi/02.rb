
use_bpm 100
use_synth :prophet

in_thread(name: :drums) do
  loop do
    cue :beat
    sample :drum_bass_hard
    sleep 1.0
  end
end


ri = 1

define :pattern do |notes, a|
  
  ##| d = 1.0/ri
  ##| ri.times do
  ##|   play notes[0], amp: a
  ##|   sleep d
  ##| end
  
  
  ##| play notes[0], amp: a
  ##| sleep 0.5
  ##| play notes[0], amp: a
  ##| sleep 0.5
  
  play notes[0], amp: a
  sleep 1.0
  
  
  ni = 1
  4.times do
    play notes[0], amp: a
    sleep 0.25
    play notes[ni], amp: a
    sleep 0.5
    
    ni = (inc ni)
  end
  
  
end


##| in_thread(name: :rythm) do
##|   sync :beat
##|   with_fx :reverb, mix: 0.05 do
##|     with_fx :echo, mix: 0.05 do
##|       loop do
##|         pattern [:G2,:G3,:G4,:G5,:G6], 0.25
##|       end
##|     end
##|   end
##| end

##| in_thread(name: :melody) do

##|   sync :beat

##|   loop do

##|     1.times do
##|       pattern [:G3,:B3,:C4,:Cs4,chord(:D4, :major7)], 0.5
##|     end
##|     1.times do
##|       pattern [:G3,:B3,:Cs4,:C4,chord(:As3, :minor)], 0.5
##|     end
##|     1.times do
##|       pattern [:G3,:B3,:C4,:Cs4,chord(:D4, :major)], 0.5
##|     end
##|     1.times do
##|       pattern [:G3,:B3,:Cs4,:C4,chord(:G4, :minor7)], 0.5
##|     end

##|     if ri == 2
##|       ri = 1
##|     else
##|       ri = (inc ri)
##|     end


##|     cue :bar

##|   end
##| end



in_thread(name: :bass) do
  sync :beat
  use_synth :pluck
  
  with_fx :flanger do |fla|
    with_fx :slicer do |sli|
      #with_fx :slicer, phase: 0.75 do
      #  with_fx :slicer, smooth_down: 1.0 do
      #    with_fx :slicer, smooth_down: 1.0, phase: 0.75 do
      
      control fla, phase: 0.75
      #control sli, phase: 0.75
      
      
      r = 0.5
      att = 0.01
      n = :G4
      p = 0.0
      a = 2.0
      bn = 0
      
      define :p_note do
        play n, attack: att, release: r, pan: rrand(-1.0, 1.0), amp: a
      end
      
      loop do
        
        p = rrand(-1.0, 1.0)
        3.times do
          p_note
          sleep 0.25
        end
        sleep 0.25
        
        p_note
        sleep 0.5
        
        2.times do
          p_note
          sleep 0.25
        end
        
        sleep 0.5
        
        if bn == 3
          use_transpose 6
          6.times do
            p_note
            sleep 0.25
          end
          bn = 0
          use_transpose 0
        else
          p_note
          sleep 0.75
          p_note
          sleep 0.75
          bn = (inc bn)
        end
      end
    end
  end
end
#  end
# end
#end

